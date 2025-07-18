// import { ScheduleGraph } from './components/ScheduleGraph/ScheduleGraph'
// import { buildScheduleGraphInfo, getErrorResponseBody } from '~/utils'
// import type { Pipeline, Schedule } from '~/types'
import type { Route } from './+types/Login'
import { SubmitHandler, useForm } from 'react-hook-form'
import styles from './Login.module.css'
import { useNavigate } from 'react-router'
import { useCallback, useEffect, useState } from 'react'
import { getUserById } from '~/api'
import { useUserStore } from '~/store/UserStore'
import { User } from '~/types'
import { AxiosError } from 'axios'
// import { ErrorCard } from '~/components/errorCard/ErrorCard'
// import { useMemo } from 'react'
// import { getSchedule } from '~/api/endpoints'
// import { Footer } from './components/Footer/Footer'
// import { hasPermission } from '~/utils'
// import { useUserStore } from '~/stores/UserStore'
// import { useScheduleStore } from '~/stores/ScheduleStore'

export const clientLoader = async ({ params }: Route.ClientLoaderArgs) => {
  // const response = await getSchedule(params.scheduleId)
  // if (!response.ok) {
  //     const data = await getErrorResponseBody(response)
  //     return {
  //         schedule: null,
  //         pipelineId: params.pipelineId,
  //         dataError: 'Failed to get schedule',
  //         response: { data, response: response },
  //     }
  // }
  // return {
  //     schedule: (await response.json()) as Schedule,
  //     pipelineId: params.pipelineId,
  //     dataError: null,
  //     response,
  // }
}

type FormData = {
  username: string
  password: string
}

const Login = ({ loaderData }: Route.ComponentProps) => {
  // const { schedule, pipelineId, dataError, response } = loaderData
  const { user, setUser } = useUserStore()
  const [loading, setLoading] = useState<boolean>(false)
  const [userError, setUserError] = useState<null | Error>(null)
  // const { pipelines, eventTypes } = useScheduleStore()

  const isTest = true

  const navigate = useNavigate()

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>()

  const routeToUser = useCallback(() => {
    if (!user) {
      return
    }
    navigate(`/user/${user.id}`)
  }, [navigate, user])

  const getUser = useCallback(
    async (id: number) => {
      try {
        setLoading(true)
        const userResponse = await getUserById(id)
        setUser((await userResponse.data) as User)
      } catch (error) {
        setUserError(error as Error)
      } finally {
        setLoading(false)
      }
    },
    [setUser]
  )

  const onSubmit: SubmitHandler<FormData> = useCallback(
    async (data) => {
      await getUser(1) //TODO update to use password
      routeToUser()
    },
    [getUser, routeToUser]
  )

  return (
    <div className={styles.wrapper}>
      <div className={styles.header}>Welcome to Clout</div>
      <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
        <div className={styles.inputContainer}>
          <div className={styles.usernameContainer}>
            <input
              {...register('username', { required: !isTest })}
              defaultValue=""
              placeholder="username"
            />
            {errors.password && <p>This field is required</p>}
          </div>
          <div className={styles.passwordContainer}>
            <input
              {...register('password', { required: !isTest })}
              placeholder="password"
            />
            {errors.password && <p>This field is required</p>}
          </div>
        </div>
        {loading ? 'isLoading' : 'Not Loading'}
        userID: {user?.id}
        <input
          type="submit"
          className={styles.submitInput}
          value={'Login'}
          disabled={loading}
        />
      </form>
    </div>
  )
}

export default Login
