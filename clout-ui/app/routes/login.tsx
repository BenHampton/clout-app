import { useCallback, useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router'
import type { Route } from './+types/login'
// import styles from './schedule.module.css'
// import { ErrorCard } from '~/components/errorCard/ErrorCard'
// import { getErrorResponseBody } from '~/utils'
// import type { Pipeline, ScheduleEventType } from '~/types'
// import { RouteGuard } from '~/routes/RouteGuard'
// import { SCHEDULER_PERMISSIONS } from '~/utils/constants'
// import { getEventTypes, getPipelines } from '~/api/endpoints'
// import { useUserStore } from '~/stores/UserStore'
// import { useScheduleStore } from '~/stores/ScheduleStore'

export const meta = () => {
    return [{ title: 'Clout UI | POC' }]
}

export const clientLoader = async () => {
    // const pipelinesPromise = getPipelines()
    // const eventTypesPromise = getEventTypes()
    //
    // const [pipelinesResponse, eventTypesResponse] = await Promise.all([
    //     pipelinesPromise,
    //     eventTypesPromise,
    // ])
    //
    // if (!pipelinesResponse.ok) {
    //     const data = await getErrorResponseBody(pipelinesResponse)
    //     return {
    //         pipelines: [],
    //         eventTypes: [],
    //         dataError: 'Failed to get pipelines',
    //         response: { data, response: pipelinesResponse },
    //     }
    // } else if (!eventTypesResponse.ok) {
    //     const data = await getErrorResponseBody(eventTypesResponse)
    //     return {
    //         pipelines: [],
    //         eventTypes: [],
    //         dataError: 'Failed to get event types',
    //         response: { data, response: eventTypesResponse },
    //     }
    // }
    //
    // return {
    //     pipelines: (await pipelinesResponse.json()) as Pipeline[],
    //     eventTypes: (await eventTypesResponse.json()) as ScheduleEventType[],
    //     dataError: null,
    //     response: pipelinesResponse,
    // }
}

const LoginRoute = ({ loaderData }: Route.ComponentProps) => {
    // const { userPermissionRoles } = useUserStore()
    // const {
    //     pipelines: loadedPipelines,
    //     eventTypes: loadedEventTypes,
    //     dataError,
    //     response,
    // } = loaderData
    // const navigate = useNavigate()
    // const { setPipelines, pipelines, setEventTypes, eventTypes } = useScheduleStore()
    //
    // useEffect(() => {
    //     setPipelines(loadedPipelines)
    // }, [loadedPipelines, setPipelines])
    //
    // useEffect(() => {
    //     setEventTypes(loadedEventTypes)
    // }, [loadedEventTypes, setEventTypes])
    //
    // const onReload = useCallback(() => {
    //     navigate(0)
    // }, [navigate])

    // if (dataError) {
    //     return (
    //         <div className={styles.wrapper}>
    //             <div className={styles.errorCardWrapper}>
    //                 <ErrorCard
    //                     title="Server Communication Error"
    //                     description={dataError}
    //                     details={response}
    //                     onRetry={onReload}
    //                 />
    //             </div>
    //         </div>
    //     )
    // }
    return (
        <main>
            <div>
                {/*login route: */}
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