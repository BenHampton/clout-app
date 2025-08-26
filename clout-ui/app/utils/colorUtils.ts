// https://gist.github.com/Accudio/b9cb16e0e3df858cef0d31e38f1fe46f
const contrastingColor = (colour: string) => {
  const rgb = hexToRGB(colour)

  // luminance of inputted colour
  const L = 0.2126 * colorMod(rgb.r) + 0.7152 * colorMod(rgb.g) + 0.0722 * colorMod(rgb.b)

  // white has a luminance of 1
  const whiteL = 1

  // contrast calculation
  const contrast = (whiteL + 0.05) / (L + 0.05)

  // return white or black depending on the contrast calculation
  return contrast > 4.5 ? '#ffffff' : '#000000'
}

// convert '#678ab4' to [r, g, b] format
const hexToRGB = (hex: string) => {
  // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
  const shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i
  hex = hex.replace(shorthandRegex, (m, r, g, b) => {
    return r + r + g + g + b + b
  })
  const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex)

  if (!result) {
    throw new Error(`Invalid hex color: ${hex}`)
  }

  return {
    r: parseInt(result[1], 16),
    g: parseInt(result[2], 16),
    b: parseInt(result[3], 16),
  }
}

// convert color in range 0-255 to the modifier used within luminance calculation
const colorMod = (color: number) => {
  const sRGB = color / 255
  let mod = Math.pow((sRGB + 0.055) / 1.055, 2.4)
  if (sRGB < 0.03928) mod = sRGB / 12.92
  return mod
}

// 'dhoenes' => '#4c2ab0'
export const colorFromString = (value: string): { color: string; contrast: string } => {
  if (typeof value !== 'string' || !value.length) {
    return {
      color: '#000000',
      contrast: '#ffffff',
    }
  }

  // https://stackoverflow.com/questions/3426404/create-a-hexadecimal-colour-based-on-a-string-with-javascript
  let hash = 0
  value.split('').forEach((character) => {
    hash = character.charCodeAt(0) + ((hash << 5) - hash)
  })
  let color = '#'
  for (let i = 0; i < 3; i++) {
    const value = (hash >> (i * 8)) & 0xff
    color += value.toString(16).padStart(2, '0')
  }

  return {
    color,
    contrast: contrastingColor(color),
  }
}

export const getRandomHexColor = () => {
  const randomColor = Math.floor(Math.random() * 16777215).toString(16)
  return `#${randomColor.padStart(6, '0')}` // Ensures 6 digits
}

export const generateRandomGradient = () => {
  const color1 = getRandomHexColor()
  const color2 = getRandomHexColor()
  const angle = Math.floor(Math.random() * 360) // Random angle 0-359 degrees

  const gradientCSS = `linear-gradient(135deg, ${color1} 0%, ${color2} 100%)`
  return gradientCSS
}
