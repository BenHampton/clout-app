import { Welcome } from '~/pages/welcome/welcome'

export const meta = () => {
  return [{ title: 'Clout UI | POC' }]
}

export const HomeRoute = () => {
  return <Welcome />
}
export default HomeRoute
