import axios, { AxiosResponse } from 'axios'
import { API_URL } from '~/utils/constants'

export const getUserById = async (userId: number): Promise<AxiosResponse> => {
  return await axios.get(`${API_URL}/v1/users/${userId}`)
}

export const getAllUserByIds = async (ids: number[]): Promise<AxiosResponse> => {
  return await axios.get(`${API_URL}/v1/users?ids=${ids.join(',')}`)
}

export const searchUserByName = async (name: string): Promise<AxiosResponse> => {
  return await axios.get(`${API_URL}/v1/users/search?name=${name}`)
}
