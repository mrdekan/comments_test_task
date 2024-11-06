import { combineReducers } from 'redux';
import userReducer from './userSlice.ts';
const rootReducer = combineReducers({
  user: userReducer,
});

export default rootReducer;
