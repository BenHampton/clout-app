import { Outlet } from 'react-router'
import type { Route } from './+types/search'

export const meta = () => {
  return [{ title: 'Clout UI | POC' }]
}

const SearchRoute = ({ loaderData }: Route.ComponentProps) => {
  return (
    <main>
      <div>
        <Outlet />
      </div>
    </main>
  )
}
export default SearchRoute
