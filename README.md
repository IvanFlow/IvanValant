# Valant

This project was generated using [Nx](https://nx.dev).

[Nx Documentation](https://nx.dev/getting-started/nx-and-angular)

[Interactive Tutorial](https://nx.dev/angular-tutorial/01-create-application)

## Get started

Run `npm install` to install the UI project dependencies. Grab a cup of coffee or your beverage of choice.
You may also need to run `npm install start-server-and-test` and `npm install cross-env`

As you build new controller endpoints you can auto generate the api http client code for angular using `npm run generate-client:server-app`

## Development server

Run `npm run start` for a dev server. Navigate to http://localhost:4200/. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng g component my-component --project=demo` to generate a new component.

## Build

Run `ng build demo` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

- Run `ng test demo` to execute the unit tests via [Jest](https://jestjs.io).
- Run `nx affected:test` to execute the unit tests affected by a change.
- Run `npm run test:all` to run all unit tests in watch mode. They will re-run automatically as you make changes that affect the tests.

## POC for Backend

It includes 5 endpoint 
1.- Save Maze: It will receive a txt file containing the definition of the maze to be stored.
  The format for this file is the proposed in the requirements document.
  This endpoint lacks of a lot of validation like row length consistency and chars allowed.
2.- Get Maze by id: It will get a Maze dto based on a List of List of chars to be managed in the UI as desired.
  This was thought to send the information using JSON format
3.- Get All Mazes: It will a List of Maze dto used in the second endpoint
  This was developed thinking in a possible "All Mazes dashboard interface on the UI" (It seems requirements mention something about it)
4.- Get Maze File by Id: It will retrieves a file related to the provided Maze Id, the file emulates the same file that was sent to the save endpoint at maze saving action.
5.- Get a list of possible moves based on a provided maze id and current position.

* NOTES:
  *  If the API is going to scale, for new folders created it would be nice to split them into several project libraries into the same solution with a proper name. Trying to apply n-layers architecture.
  * The context provided is just emulating a data source, that's why it is being inject as a singleton. Once data source selected, the Context implementation can be removed and trying to keep as much as possible the interface proposed.
  * Missing documentation on the interfaces.
  * Missing validation strategy, architecture style location for validation to be defined.
  * Missing final response management, Middleware proposed.
  * Missing exceptions management, Middleware proposed.
  * Missing Swagger configuration to take in consideration XML comments to be shown in the Swagger UI.
  * For testing: 
    * It exists a test file that propose "Integration Testing" for each endpoint, missing this.
    * It would be propose to include as well "Unit Testing" for all newly created classes that contains logic. 
  
## POC Front End

* Missing Create Maze view:
  * It will store all user changes in a model to be translated into the formatted file, the translation could be manage in the service class or in another more specific service trying to achieve Single Responsibility for that propose.
* Missing list of all Mazes created.
  * This will be the starting point for selecting a single maze and start playing.
* Missing a Maze view.
  * It can be storing, as a input property, the maze state that includes Maze Id, current position (x, y), and track path
  * It will be asking the API the for the allowed next possible steps every time the current position state suffer a change.
  * It would be analyzed if it is better to manage the state by using Observable service injected in a higher level component.
* Missing to unit test for each new component.
* Missing to accurate the new existing endpoint routes in the API.

