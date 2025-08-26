import styles from './Search.module.css'
import { searchUserByName } from '~/api'
import { Friend } from '~/types'
import { useCallback, useMemo, useState } from 'react'
import { AppButton } from '~/components/AppButton/AppButton'
import { RelationshipType } from '~/utils/constants'
import { useAppUserStore } from '~/store/UserStore'
import { useNavigate } from 'react-router'

const Search = () => {
  const { appUser } = useAppUserStore()
  const [loading, setLoading] = useState<boolean>(false)
  const [friends, setFriends] = useState<Friend[]>([])
  const [error, setError] = useState<null | Error>(null)
  const [searchQuery, setSearchQuery] = useState('')

  const navigate = useNavigate()

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(event.target.value)
  }

  const onSearch = useCallback(async () => {
    if (!appUser || !searchQuery?.length) {
      return
    }
    try {
      setLoading(true)
      const searchResponse = await searchUserByName(searchQuery, appUser?.id)
      setFriends((await searchResponse.data) as Friend[])
    } catch (error) {
      setError(error as Error)
    } finally {
      setLoading(false)
    }
  }, [searchQuery, appUser])

  const relationship = useCallback((relationshipType: number) => {
    return RelationshipType[relationshipType]
  }, [])

  const handleFriendClick = useCallback(
    (friendId: number) => {
      setSearchQuery('')
      setFriends([])

      navigate(`/${appUser?.firstName}${appUser?.lastName}/user/${friendId}`)
    },
    [appUser?.firstName, appUser?.lastName, navigate]
  )

  const searchResults = useMemo(() => {
    return (
      <>
        {!loading &&
          appUser &&
          friends?.map((friend) => (
            <div
              key={friend.id}
              onClick={() => handleFriendClick(friend.id)}
              className={styles.searchResult}
            >
              <div>
                {friend.firstName} {friend.lastName}{' '}
                {relationship(friend.relationshipType)}
                {/* TODO {friend.relationshipType} */}
              </div>
            </div>
          ))}
      </>
    )
  }, [appUser, friends, handleFriendClick, loading, relationship])

  return (
    <div>
      {/*<div className={styles.searchContainer}>*/}
      {/*<div className={styles.searchBar}>*/}
      {/*  <span className={styles.searchInput}>*/}
      {/*    <input*/}
      {/*      style={{ minWidth: '180px', height: '36px' }}*/}
      {/*      type="text"*/}
      {/*      placeholder="Search..."*/}
      {/*      value={searchQuery}*/}
      {/*      onChange={handleChange}*/}
      {/*    />*/}
      {/*    /!*{friends.length ? 'T' : 'F'}*!/*/}
      {/*    {friends?.length > 0 && (*/}
      {/*      <span className={styles.searchResultsContainer}>{searchResults}</span>*/}
      {/*    )}*/}
      {/*  </span>*/}
      {/*  <span className={styles.searchBarButton}>*/}
      {/*    <AppButton onClick={onSearch}>Search</AppButton>*/}
      {/*  </span>*/}
      {/*</div>*/}
      {/*</div>*/}
    </div>
  )
}

export default Search
