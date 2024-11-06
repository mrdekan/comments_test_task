// store.js
import { configureStore } from '@reduxjs/toolkit';
import { authApi } from '../API/auth.ts';
import rootReducer from './Reducers/rootReducer.ts';

const store = configureStore({
  reducer: {
    ...rootReducer,
    [authApi.reducerPath]: authApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authApi.middleware),
});

export default store;
