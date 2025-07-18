// import { ScheduleGraph } from './components/ScheduleGraph/ScheduleGraph'
// import { buildScheduleGraphInfo, getErrorResponseBody } from '~/utils'
// import type { Pipeline, Schedule } from '~/types'
import type { Route } from './+types/User'
import styles from './User.module.css'
import { getUserById } from '~/api'
import { User } from '~/types'
import { AppButton } from '~/components/AppButton/AppButton'
import { useCallback, useState } from 'react'
import { Wall } from '~/pages/user/components/Wall'
import { Friends } from '~/pages/user/components/friend/Friends'

export const clientLoader = async () => {
  const userResponse = await getUserById(1)
  return {
    dataError: null,
    user: (await userResponse.data) as User,
  }
}

const User = ({ loaderData }: Route.ComponentProps) => {
  const { user } = loaderData
  const [showFriends, setShowFriends] = useState<boolean>(false)

  const toggleShowFriends = useCallback(() => {
    setShowFriends(!showFriends)
  }, [setShowFriends, showFriends])

  return (
    <div className={styles.wrapper}>
      {user && (
        <>
          <div className={styles.content}>
            <div className={styles.header}>
              User
              <span className={styles.addFriendButton}>
                <AppButton onClick={() => alert('Add Friend TODO')}>Add Friend</AppButton>
              </span>
            </div>
            <div>First Name: {user.firstName}</div>
            <div>Last Name: {user.lastName}</div>
          </div>

          <div className={styles.friends}>
            <AppButton onClick={toggleShowFriends}>Show Friend</AppButton>
            {showFriends && <Friends userFriends={user.userFriends} />}
          </div>
          <div className={styles.wall}>
            <Wall />
          </div>
        </>
      )}
    </div>
  )
}

export default User
