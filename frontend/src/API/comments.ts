import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { GetCommentsResponse, GetTopLayerCommentsResponse, GetTopLevelCommentsRequest } from '../Types/comments.ts';
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
   getTopLayerComments: builder.query<GetTopLayerCommentsResponse,GetTopLevelCommentsRequest>({
      query: (data) => ({
        url: '/comments?page='+data.page+'&sort='+data.sorting,
        method: 'GET',
      }),
    }),
    getChildren: builder.query<GetCommentsResponse,number>({
      query: (id) => ({
        url: '/comments/children/'+id,
        method: 'GET',
      }),
    }),
  }),
});

export const { usePostCommentMutation, useGetTopLayerCommentsQuery, useLazyGetChildrenQuery } = commentsApi;