# Create tag
```gql
mutation{
  createTags(input:{
    name : "ts"
  }) 
  {
    tags { 
      name
    }
  }
}

```

# Create Person

```gql
mutation{
     createPeople(input:{
name: "vlad"
  }) {
    people {
      name
      tags {
        name
      }
    }
  }
}

mutation{
     createUsers(input:{name: "vlad"  }) {
    users {
      name
      titles {
        name
      }
    }
  }
}
```



#  Make relationship
```gql
mutation UpdatePeople{
  updatePeople(connect: {
    tags: {
      where : { node :  { name : "c#"}}
    }
  }) {
    people {
      name
      tags {
        name
      }
    }
  }
}
```

# Query Person with tag "C#"

```gql
query($where: PersonWhere){
  people(where: $where) {
    name
    tags {
       name
    }
  }

}

== vvariable
{
  "where": {"tags" : { "name_CONTAINS" : "c#"}}
}
```

# Update People tag
```gqlmutation {
  updatePeople(
    connect: {
      tags: {
        where: { node: { name: "scalar" } }
        connect: { persons: { where: { node: { name: "vlad" } } } }
      }
    }
  ) {
    people {
      name
      tags {
        name
      }
    }
  }
}
```





# Others query
mutation {
  createTitles(input: { name: "java" }) {
    titles {
      name
    }
  }
}

mutation {
  createUsers(input: { name: "pom" }) {
    users {
      name
      titles {
        name
      }
    }
  }
}

query {
  users(where: { name: "vlad" }) {
    titles {
      users(where: { name_NOT: "vlad" }) {
        name
      }
    }
  }
}

query {
  titles {
    name
    users {
      name
    }
  }
}

mutation {
  updateUsers(
    connect: {
      titles: {
        where: { node: { name: "java" } }
        connect: { users: { where: { node: { name: "pom" } } } }
      }
    }
  ) {
    users {
      name
      titles {
        name
      }
    }
  }
}

mutation CreateUsers($userName: String!) {
  createUsers (input: {
    name: $userName
  }) {
    info {
      nodesCreated
    }
  }
}

mutation UpdateUsers($userName: String!, $technologyName: String!){
  updateUsers(
      connectOrCreate: {
        technologies: {
            where: { node: { name: $technologyName } }
            onCreate: { node: { name: $technologyName } }
        }
      },
      where: { name: $userName }
  ) {
    info {
      nodesCreated
      relationshipsCreated
    }
  }
}

# More

mutation CreateUsers($userName: String!) {
  createUsers (input: {
    name: $userName
  }) {
    info {
      nodesCreated
    }
  }
}

query User($userName: String!) {
  users(where: { name: $userName }) {
    name
  }
}

mutation UpdateUsers($userName: String!, $technologyName: String!){
  updateUsers(
      connectOrCreate: {
        technologies: {
            where: { node: { name: $technologyName } }
            onCreate: { node: { name: $technologyName } }
        }
      },
      where: { name: $userName }
  ) {
    info {
      nodesCreated
      relationshipsCreated
    }
  }
}

mutation DetachUsers($userName: String!, $technologyName: String!){
  updateUsers(
      disconnect: {
        technologies: {
            where: { node: { name: $technologyName } }
        }
      },
      where: { name: $userName }
  ) {
    info {
      relationshipsDeleted
    }
  }
}

query GetTags {
  tags {
    id
    name
  }
}

query GetFriends($userName: String!){
  users(where: { name: $userName }){
    technologies {
      users(where: { name_NOT: $userName }){
        name
      }
    }
  }
}