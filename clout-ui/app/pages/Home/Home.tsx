import styles from './Home.module.css'
import { useAppUserStore } from '~/store/UserStore'
import { useCallback } from 'react'
import { useNavigate } from 'react-router'
import { AppButton } from '~/components/AppButton/AppButton'

export const Home = () => {
  const { appUser } = useAppUserStore()
  const navigate = useNavigate()

  const routeToUser = useCallback(() => {
    if (!appUser) {
      return
    }
    // navigate(`/user/${appUser.id}`)
    navigate(`/${appUser?.firstName}${appUser?.lastName}/profile`)
  }, [navigate, appUser])

  return (
    <div className={styles.homeContainer}>
      <h2>Welcome</h2>
      {appUser && (
        <div className={styles.userInformation}>
          {appUser?.firstName} {appUser?.lastName}
        </div>
      )}
      <div className={styles.buttonContainer}>
        <AppButton onClick={routeToUser}>Go To Profile</AppButton>
      </div>
    </div>
  )
}

export default Home
