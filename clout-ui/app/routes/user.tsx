import User from '~/routes/user'
import { Outlet } from 'react-router'
import type { Route } from './+types/user'

export const meta = () => {
  return [{ title: 'Clout UI | POC' }]
}

const UserRoute = ({ loaderData }: Route.ComponentProps) => {
  return (
    <main>
      <div>
        TEST USER
        <Outlet />
      </div>
    </main>
  )
}
export default UserRoute
