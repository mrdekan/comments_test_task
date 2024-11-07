import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { GetCommentsResponse } from '../Types/comments.ts';
const API_URL = process.env.REACT_APP_API_URL;
export const commentsApi = createApi({
  reducerPath: 'commentsApi',
  baseQuery: fetchBaseQuery({
    baseUrl: API_URL,
    credentials: 'include',
  }),
  endpoints: (builder) => ({
    postComment: builder.mutation({
      query: (formData) => ({
        url: '/comments',
        method: 'POST',
        body: formData,
      }),
    }),
   getTopLayerComments: builder.query<GetCommentsResponse,void>({
      query: () => ({
        url: '/comments',
        method: 'GET',
      }),
    }),
  }),
});

export const { usePostCommentMutation, useGetTopLayerCommentsQuery } = commentsApi;