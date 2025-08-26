import { SubmitHandler, useForm } from 'react-hook-form'
import styles from './Login.module.css'
import { useNavigate } from 'react-router'
import { useCallback, useState } from 'react'
import { getUserById } from '~/api'
import { useAppUserStore } from '~/store/UserStore'
import { User } from '~/types'

type FormData = {
  username: string
  password: string
}

export const Login = () => {
  // const { schedule, pipelineId, dataError, response } = loaderData
  const { appUser, setAppUser } = useAppUserStore()
  const [loading, setLoading] = useState<boolean>(false)
  const [userError, setUserError] = useState<null | Error>(null)

  const navigate = useNavigate()

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>()

  const routeToUser = useCallback(() => {
    if (!appUser) {
      return
    }
    navigate(`/home`)
  }, [navigate, appUser])

  const getUser = useCallback(
    async (id: number) => {
      try {
        setLoading(true)
        const userResponse = await getUserById(id)
        setAppUser((await userResponse.data) as User)
      } catch (error) {
        setUserError(error as Error)
      } finally {
        setLoading(false)
      }
    },
    [setAppUser]
  )

  const onSubmit: SubmitHandler<FormData> = useCallback(
    async (data) => {
      //todo update when create-user is implemented
      if (!appUser) {
        await getUser(1) //TODO update to use password
      }
      routeToUser()
    },
    [appUser, getUser, routeToUser]
  )

  return (
    <div className={styles.wrapper}>
      <div className={styles.header}>Welcome to Clout</div>
      {appUser && (
        <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
          <div className={styles.inputContainer}>
            <div className={styles.usernameContainer}>
              <input
                {...register('username', { required: true })}
                value={appUser?.firstName}
                placeholder="username"
              />
              {errors.password && <p>This field is required</p>}
            </div>
            <div className={styles.passwordContainer}>
              <input
                {...register('password', { required: true })}
                value={appUser?.lastName}
                placeholder="password"
              />
              {errors.password && <p>This field is required</p>}
            </div>
          </div>
          <input
            type="submit"
            className={styles.submitInput}
            value={'Login'}
            disabled={loading}
          />
        </form>
      )}
    </div>
  )
}

export default Login
