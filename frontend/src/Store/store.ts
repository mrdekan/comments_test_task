import { configureStore } from '@reduxjs/toolkit';
import { commentsApi } from '../API/comments.ts';
import rootReducer from './Reducers/rootReducer.ts';

const store = configureStore({
  reducer: {
    ...rootReducer,
    [commentsApi.reducerPath]: commentsApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(commentsApi.middleware),
});

export default store;
