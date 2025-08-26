import type { Route } from './+types/User'
import styles from './User.module.css'
import {
  acceptFriendRequest,
  createFriendRequest,
  deleteFriendRequest,
  getFriendById,
  getUserById,
} from '~/api'
import { Friend, FriendRequest, User } from '~/types'
import { AppButton } from '~/components/AppButton/AppButton'
import { useCallback, useEffect, useMemo, useState } from 'react'
import { RelationshipType } from '~/utils/constants'
import { useAppUserStore } from '~/store/UserStore'
import { ProfileBadge } from '~/components/ProfileBadge/ProfileBadge'
import { RiFacebookCircleFill, RiInstagramLine, RiTwitterXFill } from 'react-icons/ri'
import { ColorBanner } from '~/components/ProfileBanner/ColorBanner/ColorBanner'

const User = ({ params }: Route.ComponentProps) => {
  const { appUser } = useAppUserStore()
  const [friend, setFriend] = useState<null | Friend>(null)
  const [social, setSocial] = useState<null>(null)

  const fetchUser = useCallback(async (userId: number, friendId: number) => {
    //todo try-catch-finally
    const userResponse = await getFriendById(userId, friendId)
    setFriend(userResponse.data as Friend)
  }, [])

  //todo make /user-profile and use useFetchData so useEffect can be removed
  useEffect(() => {
    if (!appUser?.id || !params?.userId) {
      return
    }
    fetchUser(appUser?.id, Number(params.userId))
  }, [appUser?.id, fetchUser, params])

  const relationship = useMemo(() => {
    const relationshipTypeId = friend?.relationshipType
      ? friend?.relationshipType
      : RelationshipType.Request
    return RelationshipType[relationshipTypeId]
  }, [friend?.relationshipType])

  const handleRelationshipClick = useCallback(async () => {
    if (!appUser || !friend) {
      return
    }

    const relationshipTypeId = friend.relationshipType
      ? friend.relationshipType
      : RelationshipType.Request

    const payload: FriendRequest = {
      userIdOne: appUser.id,
      userIdTwo: friend.id,
      requestor: appUser?.id,
    }

    if (relationshipTypeId === RelationshipType.Request) {
      const friendRequest = await createFriendRequest(payload)
      const response = (await friendRequest.data) as Friend
      setFriend({ ...friend, relationshipType: response.relationshipType })
    }
    if (relationshipTypeId === RelationshipType.Accept) {
      const friendRequest = await acceptFriendRequest(payload)
      const response = (await friendRequest.data) as Friend
      setFriend({ ...friend, relationshipType: response.relationshipType })
    }
    if (
      relationshipTypeId === RelationshipType.Reject ||
      relationshipTypeId === RelationshipType.Requested
    ) {
      const friendRequest = await deleteFriendRequest(appUser.id, friend.id)
      const response = (await friendRequest.data) as Friend
      setFriend({ ...friend, relationshipType: response.relationshipType })
    }

    //require 2 user clicks somewhere in pages
    if (relationshipTypeId === RelationshipType.Unfriend) {
      //todo remove friendship new endpoint DELETE something like {{domain}}/api/v1/user-friends?userid=1
    }
  }, [appUser, friend])

  return (
    <div className={styles.userContainer}>
      {friend && (
        <div className={styles.user}>
          <div className={styles.profileBannerContainer}>
            <ColorBanner />
          </div>
          <div className={styles.profileBadge}>
            <ProfileBadge username={friend.firstName[0].concat(friend.lastName[0])} />
          </div>
          <div className={styles.social}>
            <RiFacebookCircleFill />
            <RiTwitterXFill />
            <RiInstagramLine />
          </div>
          <div className={styles.profile}>
            <div className={styles.username}>
              {friend.firstName} {friend.lastName}
            </div>
          </div>
          <div className={styles.aboutMe}>
            <div>
              <span className={styles.addFriendButton}>
                {/*todo api return relationship for user.. new endpoint since appUser wont have relationship?*/}
                <AppButton onClick={handleRelationshipClick}>{relationship}</AppButton>
              </span>
            </div>
          </div>
          <div className={styles.postContainer}>Post/Comments</div>
        </div>
      )}
    </div>
  )
}

export default User
