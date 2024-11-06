import React from "react";
import "./App.css";
import Register from "./Components/Auth/Register/Register.tsx";
import { Provider } from "react-redux";
import store from "./Store/store.ts";

function App() {
	return (
		<Provider store={store}>
			<div className="App">
				<Register />
			</div>
		</Provider>
	);
}

export default App;
