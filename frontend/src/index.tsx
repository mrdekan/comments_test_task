import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { Provider } from "react-redux";
import store from "./Store/store.ts";

const root = ReactDOM.createRoot(
	document.getElementById("root") as HTMLElement
);
root.render(<Provider store={store} children={<App />}></Provider>);
