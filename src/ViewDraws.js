import axios from "axios";
import React from "react";
import { useState, useEffect } from "react";
import { Table, Container } from 'react-bootstrap';

const ViewDraws = () => {
    const [data, setData] = useState([]);

    useEffect(() => {
        const baseURL = "http://localhost:53857/api/GamePlanner1/GetAllGameDraws";
        axios.get(baseURL)
            .then(response => {
                setData(response.data);
            })
            .catch(error => {
                console.log("Error:", error);
            });
    }, []);

    // Function to group matches by game name
    const groupMatchesByGameName = () => {
        const groupedMatches = {};
        data.forEach(match => {
            if (!groupedMatches[match.GameName]) {
                groupedMatches[match.GameName] = [];
            }
            groupedMatches[match.GameName].push(match);
        });
        return groupedMatches;
    };

    return (
        <Container className="mt-5">
            <Table responsive striped bordered hover>
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
                                        <td rowSpan={matches.length} 
                                        style={{ verticalAlign: 'middle', textAlign: 'center' }}>{gameName}</td>
                                    )}
                                    <td>{match.Team1}</td>
                                    <td>{match.Team2}</td>
                                </tr>
                            ))}
                        </React.Fragment>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
}

export default ViewDraws;
