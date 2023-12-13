import axios from 'axios';
import './App.css';
import { CatsList } from './CatList/CatList';
import { Cat } from './Models/Cat.model';
import { useEffect, useState } from 'react';

function App() {
  const [cats, setCats] = useState<Cat[]>([])
  const [cat, setCat] = useState("")

  useEffect(() => {
    axios.get<Cat[]>(`http://localhost:5125/cats`).then(resp => {
      setCats(resp.data)
    })
  }, [])

  function handleShowCat(name: string) {
    setCat(name)
  }

  return (
    <div className="App">
      <CatsList cats={cats} catClick={handleShowCat}></CatsList>
      <p>
        Wybrałeś: {cat}
      </p>
    </div>
  );
}

export default App;
