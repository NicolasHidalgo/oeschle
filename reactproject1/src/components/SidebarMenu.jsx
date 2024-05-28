import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'
import { Link } from 'react-router-dom';
/*import 'bootstrap-icons/font/bootstrap-icons.css'*/

const SidebarMenu = () => {
//function SidebarMenu() {
    return (
        <div className='sidebar d-flex flex-column justify-content-between bg-dark text-white p-4 vh-100'>
            <div>
                <a className='d-flex align-items-center text-white'>
                    <span className='ms-1 fs-4'>Menu</span>
                </a>
                <hr className='text-secondary mt-2' />
                <ul className='nav nav-pills flex-column p-0 m-0'>
                    <li className='nav-item p-1'>
                        <Link to="employees" className="nav-link text-white fs-5" aria-current="page">
                            <i className='bi bi-speedometer2'></i>
                            <span className='ms-2'>Empleados</span>
                        </Link>
                    </li>
                </ul>
            </div>
                    
        </div>
    )
}

export default SidebarMenu
