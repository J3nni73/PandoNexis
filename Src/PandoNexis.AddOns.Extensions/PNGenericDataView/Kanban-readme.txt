I Settings för GenericDataView så finns en property vid namn KanbanData (typ KanbanDataSettings).
  Den innehåller 
	* En bool (OnlyShowTaskName) - Som default är true. Om false så visas hela containerns fält som en Card (Som går att flytta). 
	* En Lista av de statusar som ska visas (GenericDataContainerStateList)
		där varje item är en GenericDataContainerState (Id, namn), vilket talar om för Kanban-vyn vilka statusar som ska visas
  
  Genom att ha OnlyShowTaskName till true så kan man välja växla mellan alla olika vyer samtidigt, där Card och Table visar/Editerar data samt Kanban bytar status
  
  På DataContainer-nivå så har vi en settings som håller en KanbanContainerData property:
   Den innehåller 
	* En TaskName - Som är namn på Workitem (Ex. "Sätt upp ramverk"). Om false så visas hela containerns fält som en Card (Som går att flytta)
	* En ContainerState (av typ GenericDataContainerState) av den status denna item har
	* En PossibleContainerStateTransitions som är en lista av strängar innehållandes de status-IDn som denna task får gå till (Kan vara både bakåt och framåt)

  Följande API-metod sätter ContainerStatus: ChangeContainerState
  
  Exempeldata finns i filen mockdataKanban.json