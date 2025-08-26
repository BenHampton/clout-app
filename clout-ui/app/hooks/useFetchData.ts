import { useCallback, useEffect, useState } from 'react'
import type { NetworkData } from '~/types'
// import { getAuthToken } from '~/api/endpoints'
export const useFetchedData = <T>(url: string, startLoading = true): NetworkData<T> => {
  const [data, setData] = useState(null as null | T)
  const [loading, setLoading] = useState(startLoading)
  const [fetchError, setFetchError] = useState(null as null | Error)
  const fetchData = useCallback(
    async (abortController?: AbortController) => {
      if (url.length) {
        setLoading(true)
        setData(null)
        setFetchError(null)
        try {
          const options: RequestInit = {
            credentials: 'include',
            headers: {
              'Content-Type': 'application/json',
              // Authorization: getAuthToken(),
            },
          }
          if (abortController) {
            options.signal = abortController.signal
          }
          const response = await fetch(url, options)
          if (response.ok) {
            const json = await response.json()
            setData(json)
          } else {
            const responseText = await response.text()
            if (response.status === 404) {
              if (responseText) {
                setData(null) // 404 due to no results
                return
              } else {
                throw new Error('404, Not found')
              }
            }
            if (response.status === 500) {
              throw new Error('500, internal server error')
            }
            // For any other server error
            throw new Error(response.status.toString())
          }
        } catch (error) {
          // set the error value unless it was from aborting the call
          if (!(error instanceof Error && error.name === 'AbortError')) {
            setFetchError(error as Error)
            // clear old data when a URL update errors out
            setData(null)
          }
        } finally {
          setLoading(false)
        }
      }
    },
    [url]
  )
  useEffect(() => {
    const abortController = new AbortController()
    fetchData(abortController)
    return () => {
      abortController.abort()
    }
  }, [fetchData])
  return {
    data,
    loading,
    error: fetchError,
    onRefetch: fetchData,
  }
}
