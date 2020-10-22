export class User {
  public id: string;
  public userName: string;
  public email: string;
  public fullName: string;

  constructor(id?: string, userName?: string, email?: string, fullName?: string) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.fullName = fullName;
  }
}

export class UserForList {
  public id: string;
  public userName: string;
  public email: string;
  public fullName: string;

  constructor(id?: string, userName?: string, email?: string, fullName?: string) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.fullName = fullName;
  }
}

export class UserSave {
  public id: string;
  public username: string;
  public email: string;
  public fullName: string;
  //public roles: string[];

  constructor(id?: string, username?: string, email?: string, fullName?: string, roles?: string[]) {
    this.id = id;
    this.username = username;
    this.email = email;
    this.fullName = fullName;
    //this.roles = roles;
  }
}

