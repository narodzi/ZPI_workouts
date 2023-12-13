import { Cat } from "../Models/Cat.model"

interface Props {
    cats: Cat[]
    catClick: (name: string) => void
}

export const CatsList = (props: Props) => {
    return (
        <>
        <ul>
            {
                props.cats.map(c => <li key={c.id}>{c.name + " " + c.race + " " + c.age + " " + c.color}
                <button onClick={() => props.catClick(c.name!)}>Wybierz</button>
                </li>)
            }
        </ul>
        </>
    )
}