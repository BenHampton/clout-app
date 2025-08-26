export interface NetworkData<T> {
  data: T | null
  onRefetch: () => void
  loading: boolean
  error: Error | null
}

export interface User {
  id: number
  firstName: string
  lastName: string
  friends: Friend[]
}

export interface Friend {
  id: number
  firstName: string
  lastName: string
  relationshipType: number
}

export interface FriendRequest {
  userIdOne: number
  userIdTwo: number
  requestor: number
}
