import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Container, Nav } from 'react-bootstrap';
import GameForm from './GameForm';
import GenerateDraw from './GenerateDraw';
import ViewDraws from './ViewDraws';
import ViewDrawGameName from './ViewDrawGameName';
import ViewPlayers from './ViewPlayers';
import Hello from './Hello';
function App() {
 
      // <Container  style={{backgroundColor:'#E6E6FA'}}>
      //   <Nav className="justify-content-center" style={{ margin: '20px 0' }}>
      //     <Nav.Item>
      //       <Link to="/" className="nav-link">
      //         GameForm
      //       </Link>
      //     </Nav.Item>
      //     <Nav.Item>
      //       <Link to="/generate" className="nav-link">
      //         Generate Draw
      //       </Link>
      //     </Nav.Item>
      //     <Nav.Item>
      //       <Link to="/view" className="nav-link">
      //         View Matches
      //       </Link>
      //     </Nav.Item>
      //     <Nav.Item>
      //       <Link to="/viewGameName" className="nav-link">
      //         View Match
      //       </Link>
      //     </Nav.Item>
      //     <Nav.Item>
      //       <Link to="/viewplayers" className="nav-link">
      //         View Players
      //       </Link>
      //     </Nav.Item>
      //   </Nav>

      //   <Routes>
      //     <Route path="/" element={<GameForm />} />
      //     <Route path="/generate" element={<GenerateDraw />} />
      //     <Route path="/view" element={<ViewDraws />} />
      //     <Route path="/viewplayers" element={<ViewPlayers/>} />
      //     <Route path="/viewGameName" element={<ViewDrawGameName/>} />
      //   </Routes>
      // </Container>
    
    return(
     
      <>
      <Hello  />
      </>
    )
   
  
}

export default App;
