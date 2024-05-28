import './App.css'
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import ShowEmployees from './components/ShowEmployees';
import SidebarMenu from './components/SidebarMenu';
//import 'bootstrap/dist/css/bootstrap.min.css'
/*import 'bootstrap-icons/font/bootstrap-icons.css'*/

function App() {
    return (
        <BrowserRouter>
            <div className='d-flex'>
                <div className='col-auto'>
                    <SidebarMenu />
                </div>
                <div>
                    <Routes>
                        <Route path='employees' element={<ShowEmployees></ShowEmployees>}></Route>
                    </Routes>
                </div>
            </div>
      </BrowserRouter>
  )
}

export default App
