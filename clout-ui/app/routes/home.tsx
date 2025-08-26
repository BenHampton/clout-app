import { Outlet } from 'react-router'
import { Header } from '~/components/Header/Header'

const UserRoute = () => {
  return (
    <div>
      <Header />
      <Outlet />
    </div>
  )
}
export default UserRoute
