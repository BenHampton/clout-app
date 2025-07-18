import { Friend } from '~/types'
import styles from './Friend.module.css'
import { useCallback, useEffect, useState } from 'react'
import { getAllUserByIds } from '~/api'

interface FriendsProps {
  userFriends: Friend[]
}
export const Friends = ({ userFriends }: FriendsProps) => {
  const [loading, setLoading] = useState<boolean>(false)
  const [friends, setFriends] = useState<Friend[]>([])
  const [error, setError] = useState<null | Error>(null)

  useEffect(() => {
    const friendIds = userFriends.map((friend) => friend.friendId)
    getFriends(friendIds)
  }, [])

  const getFriends = useCallback(async (ids: number[]) => {
    try {
      setLoading(true)
      const friendResponse = await getAllUserByIds(ids)
      setFriends((await friendResponse.data) as Friend[])
    } catch (error) {
      setError(error as Error)
    } finally {
      setLoading(false)
    }
  }, [])

  return (
    <div>
      {!loading && (
        <div className={styles.friendContainer}>
          {friends?.map((friend, index) => (
            <div key={index}>
              <div>
                {friend.firstName} {friend.lastName}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  )
}
