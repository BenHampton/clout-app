import styles from './ProfileBadge.module.css'
import { useMemo } from 'react'
import { colorFromString } from '~/utils/colorUtils'

export interface ProfileBadgeProps {
  username: string
}

export const ProfileBadge = ({ username }: ProfileBadgeProps) => {
  const shortUsername = useMemo(() => {
    const value = username?.length > 2 ? username.substring(0, 2) : username.toString()
    return value.replace(/[0-9]/g, '').toUpperCase()
  }, [username])

  const badgeColor = colorFromString(username)
  const classes = [styles.circle].join(' ')

  return (
    <div
      className={classes}
      style={{
        backgroundColor: badgeColor.color,
      }}
    >
      <span style={{ color: badgeColor.contrast }}>{shortUsername}</span>
    </div>
  )
}
