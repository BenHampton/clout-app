import styles from './Header.module.css'
import { SearchBar } from '~/components/SearchBar/SearchBar'

export const Header = () => {
  return (
    <div className={styles.headerContainer}>
      <SearchBar />
    </div>
  )
}
