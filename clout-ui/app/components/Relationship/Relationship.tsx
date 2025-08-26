import styles from './Relationship.module.css'

interface RelationshipProps {
  test?: string
}

export const Relationship = ({
  test = 'Relationship',
}: RelationshipProps) => {
  return <div className={styles.relationshipContainer}>{test}</div>
}
