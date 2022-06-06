const inputBuscar = document.getElementById('buscar')
const celdas = document.getElementsByTagName('h5')



inputBuscar.addEventListener('keyup', (e)=>{
    let texto = e.target.value
    let er = new RegExp(texto, "i")
    for(let i=0; i<celdas.length; i++) {
        let valor = celdas[i]
        //console.log(valor)
        if(er.test(valor.innerText)){
            valor.classList.remove("ocultar")
        }else{
            //console.log(valor)
            valor.classList.add("ocultar")
        }
    }
})