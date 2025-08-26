import styles from './ColorBanner.module.css'

export const ColorBanner = () => {
  const blue = ['135deg, #00adef 0%, #0076e5 100%', '135deg, #0076e5 0%, #00adef 100%']
  const purple = ['135deg, #A32EC9 0%, #621F78 100%', '135deg, #621F78 0%, #A32EC9 100%']
  const purpleBlue = [
    '135deg, #3A2FD4 0%, #11088C 100%',
    '135deg, #11088C 0%, #3A2FD4 100%',
  ]
  const teal = ['135deg, #24CED4 0%, #5DEBF0 100%', '135deg, #5DEBF0 0%, #24CED4 100%']
  const pink = ['135deg, #F51196 0%, #ED77BC 100%', '135deg, #ED77BC 0%, #F51196 100%']
  const green = ['135deg, #5CD63A 0%, #5DBD42 100%', '135deg, #5DBD42 0%, #5CD63A 100%']
  const red = ['135deg, #B00233 0%, #F03E71 100%', '135deg, #F03E71 0%, #B00233 100%']
  const linearGradients = [
    ...blue,
    ...purple,
    ...purpleBlue,
    ...teal,
    ...pink,
    ...green,
    ...red,
  ]

  const appPink = [
    '135deg, #ec5990 0%, #bf1650 100%',
    '135deg, #bf1650 0%, #ec5990 100%',
    '135deg, #bf1651 0%, #ec5991 100%',
  ]
  const randomLinearGradients = appPink[Math.floor(Math.random() * appPink.length)]
  return (
    <div
      className={styles.container}
      style={{ background: `linear-gradient(${randomLinearGradients})` }}
    />
  )
}
