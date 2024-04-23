import React from "react";
import { useState } from "react";
import './Hello.css';
const Hello=()=>{
    const s="12";
    function example(){
        alert("button clicked");
    }
    const [firstNumber,setFirstNumber]=useState(0);
    const [secondNumber,setSecondNumber]=useState(0);
    const[resut,setResult]=useState(0);
    const num=13;
    const num1=15;
    const [num2,setNum2]=useState(0);

    const branch="cse";
    const name="12";
    return(
        
        <>
        <h1 style={{color:'red',
    backgroundColor:'yellow'}}>Hello world</h1>
        <h1>{s}</h1>
        <button onClick={example}>
            click here
        </button>
        <h1>
            {num>10?'hello world':'example'}
            
        </h1>
        <h1>
        {num1===15 && 'heloo'}
        </h1>
        <center>
        <input type="number"
        placeholder="enter the number"
        value={num2}
        onChange={(e)=>{
            setNum2(e.target.value)
        }}
        />
        </center>
        <center><h1>
            {num2}
        </h1></center>
        <center>
        <input type="number" 
        placeholder="enter first number" 
        value={firstNumber}
        onChange={(e)=>{
            setFirstNumber(e.target.value);
        }}/>
    <br/>
    <br/>
<input type="number" 
        placeholder="enter second number" 
        value={secondNumber}
        onChange={(e)=>{
            setSecondNumber(e.target.value);
        }}/>
        <br/>
        <br/>
        <button onClick={(e)=>{
            setResult(Number(firstNumber)+Number(secondNumber));
        }}>
            Add
            </button>
           
           <button onClick={(e)=>{
            setResult(Number(firstNumber)-Number(secondNumber));
        }}>
            subtract
            </button> 
            
            <button onClick={(e)=>{
            setResult(Number(firstNumber)* Number(secondNumber));
        }}>
            multiply
            </button> 
         
            <button onClick={(e)=>{
            setResult(Number(firstNumber)/ Number(secondNumber));
        }}>
           divide
            </button> 
        <h1>
            The Result : {resut}
            
        </h1>
        </center>
        <center>
           <h1> {branch+name}</h1>
            <h1>{branch.length}</h1>
            <h1>{branch.toUpperCase()}</h1>
            <h1>{branch.replace('c','e')}</h1>
            <h1>{branch.charAt(0)}</h1>
        </center>
        </>
        
    )
}
export default Hello;