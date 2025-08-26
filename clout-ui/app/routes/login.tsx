import React, { useEffect } from 'react'
import { Outlet } from 'react-router'
import type { Route } from './+types/login'
import { getUserById } from '~/api'
import { User } from '~/types'
import { useAppUserStore } from '~/store/UserStore'

export const meta = () => {
  return [{ title: 'Clout UI | POC' }]
}

export const clientLoader = async () => {
  const userResponse = await getUserById(1) //todo for test only

  //todo
  // if (!userResponse.status) {
  //     const data = await getErrorResponseBody(pipelinesResponse)
  //     return {
  //         pipelines: [],
  //         eventTypes: [],
  //         dataError: 'Failed to get pipelines',
  //         response: { data, response: pipelinesResponse },
  //     }

  return {
    user: userResponse.data as User,
    dataError: null,
    response: userResponse,
  }
}

const LoginRoute = ({ loaderData }: Route.ComponentProps) => {
  const {
    user,
    // dataError,
    // response,
  } = loaderData

  const { setAppUser } = useAppUserStore()

  useEffect(() => {
    setAppUser(user)
  }, [setAppUser, user])

  return (
    <main>
      <div>
        <Outlet />
        {/*<RouteGuard*/}
        {/*    userPermission={userPermissionRoles}*/}
        {/*    permissions={SCHEDULER_PERMISSIONS}*/}
        {/*    role="Scheduler"*/}
        {/*>*/}
        {/*    {pipelines?.length && eventTypes?.length ? <Outlet /> : <></>}*/}
        {/*</RouteGuard>*/}
      </div>
    </main>
  )
}
export default LoginRoute
