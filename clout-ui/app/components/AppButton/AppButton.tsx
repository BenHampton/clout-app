import styles from './AppButton.module.css'
import type { ReactNode } from 'react'

export interface AppButtonProps {
  children: ReactNode
  type?: 'primary' | 'secondary' | 'danger'
  size?: 'sm' | 'md' | 'lg'
  disabled?: boolean
  onClick?: () => void
}

export const AppButton = ({
  children,
  type = 'primary',
  size = 'lg',
  disabled = false,
  onClick = () => {},
}: AppButtonProps) => {
  const classes = [styles.button, styles[size], styles[type]]

  return (
    <button className={classes.join(' ')} disabled={disabled} onClick={onClick}>
      {children}
    </button>
  )
}
