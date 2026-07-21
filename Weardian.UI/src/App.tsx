import { useState } from 'react'
import MainLayout from './layouts/MainLayout';
import Home from './pages/Home';
import Encrypt from './pages/Encrypt';
import KeyManagement from './pages/KeyManagement';
import './App.css'

function App() {
  const [display, setDisplay] = useState("home");

  return (
    <MainLayout setDisplay={setDisplay}>
      {display === "home" && <Home />}
      {display === "encrypt" && <Encrypt />}
      {display === "keyManagement" && <KeyManagement />}
    </MainLayout>
  )
}

export default App
