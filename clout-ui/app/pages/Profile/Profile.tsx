import styles from './Profile.module.css'
import { AppButton } from '~/components/AppButton/AppButton'
import { useCallback, useState } from 'react'
import { useAppUserStore } from '~/store/UserStore'
import { Friends } from '~/components/Friend/Friends'
import { Post } from '~/components/Post/Post'

const Profile = () => {
  const { appUser } = useAppUserStore()
  const [showFriends, setShowFriends] = useState<boolean>(false)

  const toggleShowFriends = useCallback(() => {
    setShowFriends(!showFriends)
  }, [setShowFriends, showFriends])

  return (
    <div className={styles.profileContainer}>
      {appUser && (
        <>
          <div className={styles.name}>
            {appUser.firstName} {appUser.lastName}
          </div>
          <div className={styles.friends}>
            <span className={styles.friendCount}> Friends: {appUser.friends.length}</span>
            {appUser.friends?.length > 0 && (
              <AppButton onClick={toggleShowFriends} size="md">
                Show Friends
              </AppButton>
            )}
            {showFriends && (
              <div className={styles.friendsListContainer}>
                <Friends userFriends={appUser.friends} />
              </div>
            )}
          </div>
          <div className={styles.posts}>
            <Post />
          </div>
        </>
      )}
    </div>
  )
}

export default Profile
