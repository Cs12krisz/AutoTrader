
const url = "https://localhost:7227/api/Cars";

getAllCars()


async function getAllCars(){
const tablaHely = document.getElementById("root")
    let request = await  fetch(url, {
    headers : {"Content-Type" : "applicatons/json"},
    method : "GET"
})

if (!request.ok) {
    console.log("hiba")
    return
}
const response = await request.json()

console.log(response)

const oszlopok = []

for (const element of response) {
    for (const key in element) {
        oszlopok.push(`<th>${key}</th>`)
    }
}

const  oszlopNevek = oszlopok.map((nev) => nev)


const szoveg = `<table id="tabla">
                ${tobbi}
                </table>`
tablaHely.innerHTML = szoveg

}






