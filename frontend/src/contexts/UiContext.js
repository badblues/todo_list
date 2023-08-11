import React, { createContext, Component } from "react";

export const UiContext = createContext({});

export const Sorts = {
  ALPHABETICAL: "Alphabetical",
  OLD: "Old",
  NEW: "New",
};

export class UiContextProvider extends Component {
  constructor(props) {
    super(props);
    this.state = {
      addTaskVisible: false,
      sort: Sorts.ALPHABETICAL,
      toggleAddTaskVisible: this.toggleAddTaskVisible.bind(this),
      setSort: this.setSort.bind(this),
    };
  }

  setSort(sort) {
    this.setState({
      sort: sort,
    });
  }

  toggleAddTaskVisible() {
    const visible = !this.state.addTaskVisible;
    this.setState({
      addTaskVisible: visible,
    });
  }

  render() {
    return (
      <UiContext.Provider value={this.state}>
        {this.props.children}
      </UiContext.Provider>
    );
  }
}
