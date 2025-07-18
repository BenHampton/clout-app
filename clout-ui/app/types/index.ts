export interface User {
  id: number
  firstName: string
  lastName: string
  userFriends: Friend[]
}

export interface Friend {
  friendId: number
  firstName: string
  lastName: string
  relationshipType: number
}
