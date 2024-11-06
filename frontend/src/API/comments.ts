import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
const API_URL = process.env.REACT_APP_API_URL;
export const commentsApi = createApi({
  reducerPath: 'commentsApi',
  baseQuery: fetchBaseQuery({
    baseUrl: API_URL+'/comments',
  }),
  endpoints: (builder) => ({
    postComment: builder.mutation({
      query: (formData) => ({
        url: '',
        method: 'POST',
        body: formData,
        credentials: 'include',
      }),
    }),
  }),
});

export const { usePostCommentMutation } = commentsApi;