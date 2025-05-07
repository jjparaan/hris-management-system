import { useState } from 'react';
import './App.css';

function App() {
    const [count, setCount] = useState<number>(0);

    const handleCounter = (e: MouseEvent<HTMLButtonElement>) => {
        if (e?.target?.id === "increment") {
            setCount((prevCount) => prevCount + 1)
        } else {
            setCount((prevCount) => prevCount - 1)
        }
    }

    return (
        <>
            <p id="count">{count}</p>
            <div className="buttons-container">
                <button id="increment" onClick={handleCounter}>+</button>
                <button id="decrement" onClick={handleCounter}>-</button>
            </div>
        </>
    );
}

export default App;