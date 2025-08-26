import { Friend } from '~/types'
import styles from './Friend.module.css'
import { useCallback } from 'react'
import { useNavigate } from 'react-router'
import { useAppUserStore } from '~/store/UserStore'

interface FriendsProps {
  userFriends: Friend[]
}

export const Friends = ({ userFriends }: FriendsProps) => {
  const { appUser } = useAppUserStore()
  const navigate = useNavigate()

  const handleFriendClick = useCallback(
    (friendId: number) => {
      navigate(`/${appUser?.firstName}${appUser?.lastName}/user/${friendId}`)
    },
    [appUser?.firstName, appUser?.lastName, navigate]
  )

  return (
    <div>
      {userFriends && (
        <div className={styles.friendContainer}>
          {userFriends?.map((userFriend) => (
            <div
              key={userFriend.id}
              className={styles.friend}
              onClick={() => handleFriendClick(userFriend.id)}
            >
              {userFriend.firstName} {userFriend.lastName}
            </div>
          ))}
        </div>
      )}
    </div>
  )
}
