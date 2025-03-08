import { Photo } from "./Photo"

export interface Member {
  id: number
    userName: string
    knownAs: string
    created: Date
    lastActive: Date
    age: number
    gender: string
    introduction: string
    interests: string
    lookingFor: string
    city: string
    country: string
    photoUrl: string
    photos: Photo[]
  }
