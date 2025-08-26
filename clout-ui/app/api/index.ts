import axios, { AxiosResponse } from 'axios'
import { API_URL, RelationshipType } from '~/utils/constants'
import { FriendRequest } from '~/types'

//user
export const getUserById = async (userId: number): Promise<AxiosResponse> => {
  return await axios.get(`${API_URL}/v1/users/${userId}`)
}

export const searchUserByName = async (
  name: string,
  userId: number
): Promise<AxiosResponse> => {
  return await axios.get(`${API_URL}/v1/users/search?name=${name}&userId=${userId}`)
}

//user-friends
export const getFriendById = async (
  userId: number,
  friendId: number
): Promise<AxiosResponse> => {
  return await axios.get(`${API_URL}/v1/user-friends/user/${userId}?friendId=${friendId}`)
}

export const createFriendRequest = async (
  payload: FriendRequest
): Promise<AxiosResponse> => {
  return await axios.post(`${API_URL}/v1/friend-requests`, payload)
}

export const acceptFriendRequest = async (
  payload: FriendRequest
): Promise<AxiosResponse> => {
  return await axios.put(
    `${API_URL}/v1/friend-requests?statusId=${RelationshipType.Approved}`,
    payload
  )
}

export const deleteFriendRequest = async (
  userId: number,
  friendId: number
): Promise<AxiosResponse> => {
  return await axios.delete(
    `${API_URL}/v1/friend-requests/user/${userId}?friendId=${friendId}`
  )
}
