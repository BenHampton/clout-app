import { create } from 'zustand'
import type { User } from '~/types'

type UserStore = {
  appUser: User | null
  setAppUser: (user: User) => void
}

export const useAppUserStore = create<UserStore>()((set) => ({
  appUser: null,
  setAppUser: (user: User) => set({ appUser: user }),
}))
