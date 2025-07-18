import type { Route } from './+types/Search'
import styles from './Search.module.css'
import { getAllUserByIds, getUserById, searchUserByName } from '~/api'
import { Friend, User } from '~/types'
import { useCallback, useState } from 'react'
import { AppButton } from '~/components/AppButton/AppButton'

export const clientLoader = async () => {}

const Search = ({ loaderData }: Route.ComponentProps) => {
  const [loading, setLoading] = useState<boolean>(false)
  const [friends, setFriends] = useState<Friend[]>([])
  const [error, setError] = useState<null | Error>(null)
  const [searchQuery, setSearchQuery] = useState('')

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(event.target.value)
  }

  const onSearch = useCallback(async () => {
    try {
      setLoading(true)
      const searchResponse = await searchUserByName(searchQuery)
      setFriends((await searchResponse.data) as Friend[])
    } catch (error) {
      setError(error as Error)
    } finally {
      setLoading(false)
    }
  }, [searchQuery])

  return (
    <div className={styles.searchContainer}>
      <div className={styles.searchBar}>
        <input
          style={{ minWidth: '180px', height: '36px' }}
          type="text"
          placeholder="Search..."
          value={searchQuery}
          onChange={handleChange}
        />
        <span className={styles.searchBarButton}>
          <AppButton onClick={onSearch}>Search</AppButton>
        </span>
      </div>

      <div className={styles.searchResultsContainer}>
        {!loading &&
          friends?.map((friend) => (
            <div key={friend.friendId}>
              <div className={styles.searchResult}>
                {friend.firstName} {friend.lastName}
                {/* TODO {friend.relationshipType} */}
              </div>
            </div>
          ))}
      </div>
    </div>
  )
}

export default Search
