import logo from './logo.svg';
import {Home} from './Home';
import {Designation} from './Designation';
import {Navigation} from './Navigation';
import {Employee} from './Employee';
import {BrowserRouter,Switch} from "react-router-dom"
import { Routes ,Route } from 'react-router-dom';

import './App.css';

function App() {
  return (
    <BrowserRouter>
    <div className='Container'>
      <h3 className='m-3 d-flex justify-content-center'>
        React JS cours
      </h3>
      <Navigation/>
      <Switch>
        <Route path="/" component={Home}></Route>
        <Route path="/Designation" component={Designation}></Route>
        <Route path="/Employee" component={Employee}></Route>
      </Switch>
    </div>
    </BrowserRouter>
  );
}

export default App;
