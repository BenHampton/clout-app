export const API_URL = `${import.meta.env.VITE_API_URL}/api`

export enum RelationshipType {
  'Friend' = 1,
  'Unfriend' = 2,
  'Request' = 3,
  'Accept' = 4, //PENDING_INCOMING
  'Requested' = 5, //PENDING_OUTGOING
  'Approved' = 6, //APPROVED_REQUEST
  'Reject' = 7,
  'Block' = 8,
  'Blocked' = 9,
}
