import React, { useState, useEffect } from 'react';
import { Table, Form, Button, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios';

const GenerateDraw = () => {
  const [tableData, setTableData] = useState([]);
  const [gameName, setGameName] = useState('');
  const [gameNames, setGameNames] = useState([]);
  const [idExist, setIdExist] = useState(false);

  useEffect(() => {
    fetchGameNames();
  }, []);

  const fetchGameNames = () => {
    axios.get('http://localhost:53857/api/GamePlanner1/GetAllGameNames')
      .then(response => {
        setGameNames(response.data);
      })
      .catch(error => {
        console.error('Error fetching game names:', error);
      });
  };

  const handleInputChange = (e) => {
    setGameName(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (gameName.trim() !== '') {
      fetchData();
    } else {
      console.error('Please select a game name.');
    }
  };

  const fetchData = () => {
    axios.get(`http://localhost:53857/api/GamePlanner1/GenerateDraw/${gameName}`)
      .then(response => {
        const data = response.data;
        setTableData(data);
        setIdExist(true);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
        setTableData([]);
      });
  };

  const handleSave = () => {
    const baseURL = "http://localhost:53857/api/GamePlanner1/SaveDraw/";
    
    axios.post(`${baseURL}${gameName}`, tableData)
      .then(response => {
        console.log(response.data);
        alert("Matches saved successfully");
      })
      .catch(error => {
        console.error('Error saving data:', error);
        alert("Error saving matches");
      });
  };

  // Function to group matches by game name
  const groupMatchesByGameName = () => {
    const groupedMatches = {};
    tableData.forEach(match => {
      if (!groupedMatches[match.GameName]) {
        groupedMatches[match.GameName] = [];
      }
      groupedMatches[match.GameName].push(match);
    });
    return groupedMatches;
  };

  return (
    <Container className="mt-5">
      <Form className="border p-4 rounded" onSubmit={handleSubmit}>
        <Row className="mb-3">
          <Col xs={9}>
            <select
              value={gameName}
              onChange={handleInputChange}
              className="form-select"
              style={{ width: '100%' }}
            >
              <option value="">Select a Game</option>
              {gameNames.map((gameName, index) => (
                <option key={index}>{gameName}</option>
              ))}
            </select>
          </Col>
          <Col xs={3}>
            <Button variant="primary" type="submit" className="w-100">
              Generate Draw
            </Button>
          </Col>
        </Row>
        <Row>
          {idExist && (
            <Col>
              <Table striped bordered hover responsive="md">
                <thead>
                  <tr>
                    <th>Game Name</th>
                    <th>Player 1</th>
                    <th>Player 2</th>
                  </tr>
                </thead>
                <tbody>
                  {Object.entries(groupMatchesByGameName()).map(([gameName, matches], index) => (
                    <React.Fragment key={index}>
                      {matches.map((match, idx) => (
                        <tr key={idx}>
                          {idx === 0 && (
                            <td rowSpan={matches.length} style={{ verticalAlign: 'middle', textAlign: 'center' }}>{gameName}</td>
                          )}
                          <td>{match.Team1}</td>
                          <td>{match.Team2}</td>
                        </tr>
                      ))}
                    </React.Fragment>
                  ))}
                </tbody>
              </Table>
              <Button type='button' variant='primary' onClick={handleSave}>Save Draw</Button>
            </Col>
          )}
        </Row>
      </Form>
    </Container>
  );
};

export default GenerateDraw;
