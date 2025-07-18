import { useEffect, useState } from 'react'
import type { NetworkData } from '~/types'
import { getAuthToken } from '~/api/endpoints'

export const useFetchedData = <T>(url: string, startLoading = true): NetworkData<T> => {
    const [data, setData] = useState(null as null | T)
    const [loading, setLoading] = useState(startLoading)
    const [fetchError, setFetchError] = useState(null as null | Error)

    useEffect(() => {
        const abortController = new AbortController()

        const fetchData = async () => {
            if (url.length) {
                try {
                    const response = await fetch(url, {
                        credentials: 'include',
                        headers: {
                            'Content-Type': 'application/json',
                            Authorization: getAuthToken(),
                        },
                        signal: abortController.signal,
                    })

                    if (response.ok) {
                        const json = await response.json()
                        setData(json)
                    } else {
                        const responseText = await response.text()
                        if (response.status === 404) {
                            if (responseText) {
                                setData(null) // 404 due to no results
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
                    setFetchError(error as Error)

                    // clear old data when a URL update errors out
                    setData(null)
                } finally {
                    setLoading(false)
                }
            }
        }

        fetchData()

        return () => {
            abortController.abort()
        }
    }, [url])

    return {
        data,
        loading,
        error: fetchError,
    }
}