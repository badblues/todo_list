import React, { createContext, Component } from "react";

export const UiContext = createContext({});

export class UiContextProvider extends Component {
  constructor(props) {
    super(props);
    this.state = {
      addTaskVisible: false,
      toggleAddTaskVisible: this.toggleAddTaskVisible.bind(this)
    };
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
