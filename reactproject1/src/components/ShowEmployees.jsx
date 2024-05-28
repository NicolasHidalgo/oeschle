import React, { useEffect, useState } from 'react';
import axios from 'axios';

const ShowEmployees = () => {
    const url = 'http://localhost:5275/api/DEMO/Get';
    const [employees, setEmployees] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [employeesPerPage] = useState(10); 

    useEffect(() => {
        getEmployees();
    }, []);

    const getEmployees = async () => {
        const respuesta = await axios.get(url);
        setEmployees(respuesta.data);
    }

    const indexOfLastEmployee = currentPage * employeesPerPage;
    const indexOfFirstEmployee = indexOfLastEmployee - employeesPerPage;
    const currentEmployees = employees.slice(indexOfFirstEmployee, indexOfLastEmployee);
    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    return (
        <div className='App'>
            <div className='container-fluid'>
                <h2>Lista de Empleados</h2>
                <div className='row mt-3'>
                    <div className='table-responsive'>
                        <table className='table table-bordered'>
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Nombre</th>
                                    <th>Num. Documento</th>
                                    <th>Salario</th>
                                    <th>Edad</th>
                                    <th>Perfil</th>
                                    <th>Fecha Admision</th>
                                </tr>
                            </thead>
                            <tbody className='table-group-divider'>
                                {currentEmployees.map(employee => (
                                    <tr key={employee.id}>
                                        <td>{employee.id}</td>
                                        <td>{employee.name}</td>
                                        <td>{employee.document_number}</td>
                                        <td>{employee.salary}</td>
                                        <td>{employee.age}</td>
                                        <td>{employee.profile}</td>
                                        <td>{employee.admission_date}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        <Pagination
                            employeesPerPage={employeesPerPage}
                            totalEmployees={employees.length}
                            paginate={paginate}
                        />
                    </div>
                </div>
            </div>
            

        </div>
    )

}

const Pagination = ({ employeesPerPage, totalEmployees, paginate }) => {
    const pageNumbers = [];

    for (let i = 1; i <= Math.ceil(totalEmployees / employeesPerPage); i++) {
        pageNumbers.push(i);
    }

    return (
        <nav>
            <ul className="pagination">
                {pageNumbers.map(number => (
                    <li key={number} className="page-item">
                        <a onClick={() => paginate(number)} href="#" className="page-link">
                            {number}
                        </a>
                    </li>
                ))}
            </ul>
        </nav>
    );
};

export default ShowEmployees